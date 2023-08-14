import { AbstractType, Component, OnInit, ViewChild } from '@angular/core';
import { ChartConfiguration, ChartData, ChartEvent, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { TransporterService } from '../transporter.service';
import { Vehiclerevenue } from '../vehiclerevenue';
import 'chartjs-plugin-datalabels';
import { YearRevenue } from 'src/app/year-revenue';

@Component({
  selector: 'app-transporterbarchart',
  templateUrl: './transporterbarchart.component.html',
  styleUrls: ['./transporterbarchart.component.css']
})
export class TransporterbarchartComponent implements OnInit {
  transporterId: any;
  years:any[]=[]
  selectedYear: number = new Date().getFullYear();
  vehicleRevenues: Vehiclerevenue[] = []
  constructor(private svc: TransporterService) { }
  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;

  public barChartOptions: ChartConfiguration['options'] = {
    responsive: true,

    // We use these empty structures as placeholders for dynamic theming.
    scales: {
      x: {},
      y: {
        min: 500,
        max: 10000,
        ticks: {
          stepSize: 500,
        },
      },
    },
    plugins: {
      legend: {
        display: true,
      },
      datalabels: {
        anchor: 'end',
        align: 'end',
      },
    },
  };
  public barChartType: ChartType = 'bar';
  public barChartPlugins = [];

  public barChartData: ChartData<'bar'> = {
    labels: [],
    datasets: [
      {
        data: [],
        label: 'vehicles'
      },
    ],
  };


  ngOnInit(): void {
    this.transporterId = Number(localStorage.getItem("transporterId"));
    // this.svc.getVehicleRevenues(this.transporterId, 2023).subscribe((res) => {
    //   this.vehicleRevenues = res
    //   console.log(res)
    //   this.barChartData.labels = this.vehicleRevenues.map((revenues) => revenues.rtoNumber);
    //   this.barChartData.datasets[0].data = this.vehicleRevenues.map((revenues) => revenues.amount);
    //   this.barChartData.datasets[0].backgroundColor = this.getBarColors(this.vehicleRevenues.length);  
    this.fetchVehicleRevenues(this.selectedYear);
    this.svc.getYears(this.transporterId).subscribe((res)=>{
      this.years=res
      console.log(this.years)
    })
    }
  fetchVehicleRevenues(year: number): void {
    this.svc.getVehicleRevenues(this.transporterId, year).subscribe((res) => {
      this.vehicleRevenues = res;
      this.barChartData.labels = this.vehicleRevenues.map((revenues) => revenues.rtoNumber);
      this.barChartData.datasets[0].data = this.vehicleRevenues.map((revenues) => revenues.amount);
      this.barChartData.datasets[0].backgroundColor = this.getBarColors(this.vehicleRevenues.length);
    });
  }
  onYearChange(newYear: number): void {
    this.selectedYear = newYear;
    this.fetchVehicleRevenues(newYear);
  }


  getBarColors(dataLength: number): string[] {
    const colors: string[] = [];
    for (let i = 0; i < dataLength; i++) {
      colors.push(this.getRandomColor());
    }
    return colors;
  }
  getRandomColor(): string {
    const letters = "0123456789ABCDEF";
    let color = "#";
    for (let i = 0; i < 6; i++) {
      color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
  }
}
