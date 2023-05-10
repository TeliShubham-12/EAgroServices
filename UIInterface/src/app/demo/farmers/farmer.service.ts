import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Farmer } from './farmer';
import { Farmersell } from './farmersell';
import { Purchaseviewmodel } from './purchaseviewmodel';

@Injectable({
  providedIn: 'root'
})
export class FarmerService {
  constructor(private httpClient:HttpClient) { }
  getFarmer(farmerId:number):Observable<any>{
    let url =" http://localhost:5141/api/farmers/getdetails/" +farmerId;
    return this.httpClient.get<Farmer>(url);
  }
  getFarmerRevenue(farmerId:number):Observable<any>{
    let url =" http://localhost:5171/api/purchase/farmerselltotalamountbymonth/" +farmerId;
    return this.httpClient.get<Farmersell[]>(url);
  }
<<<<<<< HEAD
  updateFarmerDetails(farmerId:number,farmer:Farmer):Observable<any>{
    let url =" http://localhost:5141/api/farmers/update/" +farmerId;
    return this.httpClient.put<Farmer>(url,farmer);
  }
  getFarmerDetails(farmerId:number):Observable<any>{
    let url =" http://localhost:5141/api/farmers/update/" +farmerId;
    return this.httpClient.get<Farmer>(url)
  }

=======

  getFarmerPurchaseDetails(farmerId:number):Observable<any>{
    let url =" http://localhost:5171/api/purchase/GetFarmerPurchaseDetails/" +farmerId;
    return this.httpClient.get<Purchaseviewmodel[]>(url);
  }
  
>>>>>>> db8f55e41c736e46adec36819d2b27ef5eba8897
}
