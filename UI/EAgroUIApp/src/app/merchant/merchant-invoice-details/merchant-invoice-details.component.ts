import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/Shared/users/user.service';
import { CorporateService } from 'src/app/corporate.service';
import { InvoicesService } from 'src/app/invoices.service';
import { InvoiceDetails } from '../invoice-details';
import { BankingService } from 'src/app/banking.service';
import { AccountInfo } from '../account-info';
import { PaymentTransferDetails } from '../payment-transfer-details';
import { PaymentService } from 'src/app/payment.service';
import { FarmerServicePayment } from 'src/app/farmer-service-payment';

@Component({
  selector: 'app-merchant-invoice-details',
  templateUrl: './merchant-invoice-details.component.html',
  styleUrls: ['./merchant-invoice-details.component.css']
})
export class MerchantInvoiceDetailsComponent implements OnInit {
  invoiceId: string | any;
  invoiceDetails!: InvoiceDetails;
  showPayment: boolean = false;
  farmerAccountInfo: AccountInfo = {
    accountNumber: '',
    ifscCode: ''
  };
  collectionCenterAccountInfo: AccountInfo = {
    accountNumber: '',
    ifscCode: ''
  };

  merchantAccountInfo: AccountInfo = {
    accountNumber: '',
    ifscCode: ''
  };
  constructor(private invoicesvc: InvoicesService, private corpsvc: CorporateService,
    private usrsvc: UserService, private banksvc: BankingService,private paymentsvc:PaymentService,
    private route: ActivatedRoute, private router: Router) { }


  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.invoiceId = params.get('invoiceid');
    });

    this.invoicesvc.getInvoiceDetails(this.invoiceId).subscribe((res) => {
      this.invoiceDetails = res;

      let ids: number[] = [this.invoiceDetails.collectionCenterId, this.invoiceDetails.transporterId];
      let idString = ids.join(',');


      this.corpsvc.getCorporates(idString).subscribe((names: any[]) => {
        console.log("🚀 ~ this.corpsvc.getCorporates ~ names:", names);
        this.invoiceDetails.collectionCenterName = names[0].name
        this.invoiceDetails.transporterName = names[1].name

      });

      let farmerId: string = this.invoiceDetails.farmerId.toString();
      this.usrsvc.getUserNamesWithId(farmerId).subscribe((response: any[]) => {
        this.invoiceDetails.farmerName = response[0].name
      });

    });
  }

  updateRate(invoiceId: number) {

    let body = { "ratePerKg": this.invoiceDetails.ratePerKg }
    this.invoicesvc.updateRate(invoiceId, body).subscribe((res) => {
      console.log("🚀 ~ this.invoicesvc.updateRate ~ res:", res);
      window.location.reload();
    });
  }

  onClickPaymentDetails() {
    this.banksvc.getFarmerAccountInfo(this.invoiceDetails.farmerId).subscribe((res) => {
      console.log("🚀 ~ onClickPay ~ res: FArmerACCount", res);
      this.farmerAccountInfo.accountNumber = res.accountNumber;
      this.farmerAccountInfo.ifscCode = res.ifscCode;
    });

    this.banksvc.getCorporateAccountInfo(this.invoiceDetails.collectionCenterId).subscribe((res) => {
      console.log("🚀 ~ onClickPay ~ res: COLLECTIONCENTER ", res);
      this.collectionCenterAccountInfo.accountNumber = res.accountNumber;
      this.collectionCenterAccountInfo.ifscCode = res.ifscCode;
    });

    this.banksvc.getCorporateAccountInfo(7).subscribe((res) => { // here 7 is corporate id of merchant
      console.log("🚀 ~ onClickPay ~ res: merchant ", res);
      this.merchantAccountInfo.accountNumber = res.accountNumber;
      this.merchantAccountInfo.ifscCode = res.ifscCode;
    });

    this.showPayment = true;
  }

  onClickPay() {
    let farmerPaymentTransfer: PaymentTransferDetails = {
      fromAcct: this.merchantAccountInfo.accountNumber,
      toAcct: this.farmerAccountInfo.accountNumber,
      fromIfsc: this.merchantAccountInfo.ifscCode,
      toIfsc: this.farmerAccountInfo.ifscCode,
      amount: this.invoiceDetails.totalAmount
    }

    this.banksvc.fundTransfer(farmerPaymentTransfer).subscribe((res)=>{
    if(res!=0)
    {

      let farmerPayment:FarmerServicePayment={
        collectionId: this.invoiceDetails.collectionId,
        transactionId: res,
        amount: farmerPaymentTransfer.amount,
        paymentFor: "farmer"
      };


      this.paymentsvc.addpayment(farmerPayment).subscribe((response) => {
        console.log("🚀 ~ this.paymentsvc.addpayment ~ response: farmerpayment", response);
      });
    }
    else
    console.log("error while transfering funds to farmer");
    });

    let serviceOwnerPaymentTransfer: PaymentTransferDetails = {
      fromAcct: this.merchantAccountInfo.accountNumber,
      toAcct: this.collectionCenterAccountInfo.accountNumber,
      fromIfsc: this.merchantAccountInfo.ifscCode,
      toIfsc: this.collectionCenterAccountInfo.ifscCode,
      amount: this.invoiceDetails.serviceCharges + this.invoiceDetails.labourCharges
    }

    this.banksvc.fundTransfer(serviceOwnerPaymentTransfer).subscribe((res)=>{
      if(res!=0)
      {
  
        let serviceOwnerPayment:FarmerServicePayment={
          collectionId: this.invoiceDetails.collectionId,
          transactionId: res,
          amount: serviceOwnerPaymentTransfer.amount,
          paymentFor: "serviceowner"
        };
  
  
        this.paymentsvc.addpayment(serviceOwnerPayment).subscribe((response) => {
          console.log("🚀 ~ this.paymentsvc.addpayment ~ response: servicepayment", response);
          window.location.reload;
        });
      }
      else
      console.log("error while transfering funds to service owner");
      });

  }
}
