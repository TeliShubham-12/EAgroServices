import { Component, Input } from '@angular/core';
import { Transport } from 'src/app/Models/transport';
import { EmployeeService } from 'src/app/Services/employee.service';
import { TransportService } from 'src/app/Services/transport.service';

@Component({
  selector: 'emp-tranport-details',
  templateUrl: './tranport-details.component.html',
  styleUrls: ['./tranport-details.component.scss']
})
export class TranportDetailsComponent {
  @Input() transport: Transport;
  merchantId: any;
  updateStatus: boolean = false;
  deleteStatus: boolean = false;
  purchaseListStatus: boolean = false;

  constructor(private transportsvc: TransportService, private empsvc: EmployeeService) {
  }

  ngOnInit(): void {

  };

  confirm() {
    this.transportsvc.deleteTransport(this.transport.transportId).subscribe((response) => {
      console.log(response)
      this.empsvc.sendRole({ selectedRole: "Transport" });
    })
  }
  onUpdateClick() {
    this.updateStatus = true;
    this.deleteStatus = false;
    this.purchaseListStatus = false;

  }
  onDeleteClick() {
    this.updateStatus = false;
    this.deleteStatus = true;
    this.purchaseListStatus = false;

  }
  onCancelClick() {
    this.deleteStatus = false;
  }
  onSellListClick() {
    this.purchaseListStatus = true;
    this.updateStatus = false;
    this.deleteStatus = false;
  }
}

