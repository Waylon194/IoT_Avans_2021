import { Component, OnDestroy } from '@angular/core';
import { NbThemeService } from '@nebular/theme';

import { Electricity, ElectricityChart, ElectricityData } from '../../../@core/data/electricity';
import { takeWhile } from 'rxjs/operators';
import { forkJoin } from 'rxjs';

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

  constructor(private electricityService: ElectricityData,
              private themeService: NbThemeService) {
    this.themeService.getJsTheme()
      .pipe(takeWhile(() => this.alive))
      .subscribe(theme => {
        this.currentTheme = theme.name;
    });

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
