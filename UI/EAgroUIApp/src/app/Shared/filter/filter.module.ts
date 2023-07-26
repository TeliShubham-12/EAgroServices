import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FilterTestComponent } from './filter-test/filter-test.component';
import { SortByComponent } from './sort-by/sort-by.component';
import { EqualFilterComponent } from './equal-filter/equal-filter.component';
import { DateFilterComponent } from './date-filter/date-filter.component';
import { RangeFilterComponent } from './range-filter/range-filter.component';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CollectionListComponent } from "../../collectioncenter/collection-list/collection-list.component";



@NgModule({
    declarations: [
        FilterTestComponent,
        SortByComponent,
        EqualFilterComponent,
        DateFilterComponent,
        RangeFilterComponent,
    ],
    exports: [
        FilterTestComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpClientModule,
        CommonModule,
        CollectionListComponent
    ]
})
export class FilterModule { }
