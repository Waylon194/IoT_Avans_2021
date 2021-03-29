import { Component, OnDestroy } from '@angular/core';
import { NbThemeService } from '@nebular/theme';

import { Electricity, ElectricityChart, ElectricityData } from '../../../@core/data/electricity';
import { takeWhile } from 'rxjs/operators';
import { forkJoin } from 'rxjs';
import { IwsnBackendService } from '../../../iwsn-backend/iwsn-backend.service'; 
import { Datagram } from '../../../models/Datagram';

@Component({
  selector: 'ngx-electricity',
  styleUrls: ['./electricity.component.scss'],
  templateUrl: './electricity.component.html',
})
export class ElectricityComponent implements OnDestroy {
  private alive = true;

  listData: Electricity[];
  chartData: ElectricityChart[];
  currentTheme: string;
  themeSubscription: any;
  measurements: Datagram[] = [];

  constructor(private electricityService: ElectricityData,
              private themeService: NbThemeService, private backend_service: IwsnBackendService) {
    this.themeService.getJsTheme()
      .pipe(takeWhile(() => this.alive))
      .subscribe(theme => {
        this.currentTheme = theme.name;
    });

    //console.log("Data recieved: " + backend_service.getMeasurements().subscribe(measurements=> this.measurements = measurements));

    forkJoin(
      this.electricityService.getChartData(),
    )
      .pipe(takeWhile(() => this.alive))
      .subscribe(([chartData]: [ElectricityChart[]] ) => {
        this.chartData = chartData;
      });
  }

  ngOnDestroy() {
    this.alive = false;
  }
}