import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { DefaultModule } from './default/default.module';
import { AuthenticationModule, authRoutes } from './Shared/authentication/authentication.module';
import { MembershipModule, membershipRoutes } from './membership/membership.module';
import { CollectioncenterModule, collectionCenterRoutes } from './collectioncenter/collectioncenter.module';
import { MerchantModule, merchantRoutes } from './merchant/merchant.module';
import { FarmerModule, farmerRoutes } from './farmer/farmer.module';
import { TransporterModule, transporterRoutes } from './transporter/transporter.module';


const routes: Routes = [
  { path: 'membership', children: membershipRoutes },
  { path: 'auth', children: authRoutes },
  { path: 'farmer', children: farmerRoutes },
  { path: 'merchant', children: merchantRoutes },
  { path: 'transporter/:id', children: transporterRoutes },
  { path: 'collectioncenter', children: collectionCenterRoutes },
]
@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    DefaultModule,
    RouterModule.forRoot(routes),
    AuthenticationModule,
    MembershipModule,
    CollectioncenterModule,
    MerchantModule,
    FarmerModule,
    TransporterModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
