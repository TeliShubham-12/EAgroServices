using System.Globalization;
using Microsoft.EntityFrameworkCore;
using TransportsAPI.Context;
using TransportsAPI.Models;
using TransportsAPI.Repositories.Interfaces;

namespace TransportsAPI.Repositories;

public class TransportRepository : ITransportRepository
{
    private IConfiguration _configuration;

    public TransportRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<Transport>> GetAll()
    {
        try
        {
            using (var context = new TransportContext(_configuration))
            {
                var transports = await context.Transports.ToListAsync();
                if (transports == null)
                {
                    return null;
                }
                return transports;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw e;
        }
    }

    public async Task<Transport> GetById(int transportId)
    {
        try
        {
            using (var context = new TransportContext(_configuration))
            {
                Transport? transport = await context.Transports.FindAsync(transportId);
                if (transport == null)
                {
                    return null;
                }
                return transport;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw e;
        }
    }

    public async Task<bool> Insert(User user, Transport transport, UserRole userRole)
    {
        bool status = false;
        int userId = 0;
        try
        {
            using (var context = new TransportContext(_configuration))
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                userId = user.Id;
                transport.UserId = userId;
                userRole.UserId = userId;
                await context.Transports.AddAsync(transport);
                await context.UserRoles.AddAsync(userRole);
                await context.SaveChangesAsync();
                status = true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw e;
        }
        return status;
    }

    public async Task<bool> Update(int transportId, Transport transport)
    {
        bool status = false;
        try
        {
            using (var context = new TransportContext(_configuration))
            {
                Transport? oldTransport = await context.Transports.FindAsync(transportId);
                if (oldTransport != null)
                {
                    oldTransport.FirstName = transport.FirstName;
                    oldTransport.LastName = transport.LastName;
                    oldTransport.OfficeName = transport.OfficeName;
                    oldTransport.Location = transport.Location;
                    await context.SaveChangesAsync();
                    status = true;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw e;
        }
        return status;
    }

    public async Task<bool> Delete(int transportId)
    {
        bool status = false;
        try
        {
            using (var context = new TransportContext(_configuration))
            {
                Transport? transport = await context.Transports.FindAsync(transportId);
                if (transport != null)
                {
                    context.Transports.Remove(transport);
                    await context.SaveChangesAsync();
                    status = true;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return status;
    }

    // for records of a Transport fare details -used in TransportList
    public async Task<List<TransportFareDetails>> TransportHistory(int transportId)
    {
        try
        {
            using (var context = new TransportContext(_configuration))
            {
                var transportHistory = await (
                    from transport in context.Transports
                    join transportTruck in context.Trucks
                        on transport.Id equals transportTruck.TransportId
                    join sell in context.Sells on transportTruck.Id equals sell.TruckId
                    join billing in context.Billings on sell.Id equals billing.SellId
                    join freightRate in context.FreightRates
                        on billing.Id equals freightRate.BillId
                    where transport.Id == transportId
                    orderby billing.Date descending
                    select new TransportFareDetails()
                    {
                        TruckNumber = transportTruck.TruckNumber,
                        FromDestination = freightRate.FromDestination,
                        ToDestination = freightRate.ToDestination,
                        Kilometers = freightRate.Kilometers,
                        RatePerKm = freightRate.RatePerKm,
                        FreightCharges = billing.FreightCharges,
                        Date = billing.Date
                    }
                ).ToListAsync();
                return transportHistory;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    //used for  column chart-one truck revenue per month of a year
    public async Task<List<TransportTruckHistory>> TransportTruckHistoryByMonth(int transportId)
    {
        try
        {
            using (var context = new TransportContext(_configuration))
            {
                var transportHistory = await (
                    from sells_billing in context.Billings
                    join sells in context.Sells on sells_billing.SellId equals sells.Id
                    join transport_trucks in context.Trucks
                        on sells.TruckId equals transport_trucks.Id
                    join transports in context.Transports
                        on transport_trucks.TransportId equals transports.Id
                    where transports.Id == transportId
                    group sells_billing by new
                    {
                        sells_billing.Date.Month,
                        transport_trucks.TruckNumber,
                        sells_billing.Date.Year
                    } into g
                    orderby g.Key.Month
                    select new TransportTruckHistory()
                    {
                        TotalFreightCharges = g.Sum(s => s.FreightCharges),
                        Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month),
                        TruckNumber = g.Key.TruckNumber,
                        Year = g.Key.Year
                    }
                ).ToListAsync();

                return transportHistory;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    //used for piechart -all trucks revenue per year of one transport
    public async Task<List<TransportTruckHistory>> TransportTruckHistoryByYear(int transportId)
    {
        try
        {
            using (var context = new TransportContext(_configuration))
            {
                var transportHistory = await (
                    from sells_billing in context.Billings
                    join sells in context.Sells on sells_billing.SellId equals sells.Id
                    join transport_trucks in context.Trucks
                        on sells.TruckId equals transport_trucks.Id
                    join transports in context.Transports
                        on transport_trucks.TransportId equals transports.Id
                    where transports.Id == transportId
                    group sells_billing by new
                    {
                        transport_trucks.TruckNumber,
                        sells_billing.Date.Year
                    } into g
                    select new TransportTruckHistory()
                    {
                        TotalFreightCharges = g.Sum(s => s.FreightCharges),
                        TruckNumber = g.Key.TruckNumber,
                        Year = g.Key.Year
                    }
                ).ToListAsync();

                return transportHistory;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    // truck orders count  per month of a transport
    public async Task<List<TransportOrderCount>> TransportTruckOrdersPerMonth(int transportId)
    {
        try
        {
            using (var context = new TransportContext(_configuration))
            {
                var transportOrdersCount = await (
                    from sell in context.Sells
                    join truck in context.Trucks on sell.TruckId equals truck.Id
                    where truck.TransportId == transportId
                    group sell by new
                    {
                        sell.Date.Year,
                        sell.Date.Month,
                        sell.TruckId
                    } into billingGroup
                    orderby billingGroup.Key.Month, billingGroup.Key.Year descending
                    select new TransportOrderCount()
                    {
                        Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(
                            billingGroup.Key.Month
                        ),
                        Year = billingGroup.Key.Year,
                        OrderCount = billingGroup.Count(),
                        TruckNumber = (
                            from truck in context.Trucks
                            where truck.Id == billingGroup.Key.TruckId
                            select truck.TruckNumber
                        ).FirstOrDefault()
                    }
                ).ToListAsync();
                return transportOrdersCount;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    // for getting all trucks of transport
    public async Task<List<Truck>> GetTransportsTrucks(int transportId)
    {
        try
        {
            using (var context = new TransportContext(_configuration))
            {
                var transportTrucks = await (
                    from transports in context.Transports
                    join transportTruck in context.Trucks
                        on transports.Id equals transportTruck.TransportId
                    where transports.   Id == transportId
                    select transportTruck
                ).ToListAsync();
                return transportTrucks;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}
