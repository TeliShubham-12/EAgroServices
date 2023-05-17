using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace TransportsAPI.Models;
public class Billing
{
    private double freightCharges;

    [Column("bill_id")]
    public int BillId { get; set; }

    [Column("sell_id")]
    public int SellId { get; set; }

    [Column("freight_charges")]
    public double FreightCharges { get => freightCharges=Math.Round(freightCharges,2); set => freightCharges = value; }

    [Column("labour_charges")]
    public int LabourCharges { get; set; }

    [Column("total_charges")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int TotalCharges { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }

    public Billing(){
        Date=DateTime.Now;
    }
}