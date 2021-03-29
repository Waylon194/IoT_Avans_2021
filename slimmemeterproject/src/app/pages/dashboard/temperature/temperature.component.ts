import { Component, OnDestroy } from '@angular/core';
import { NbThemeService } from '@nebular/theme';
import { TemperatureHumidityData } from '../../../@core/data/temperature-humidity';
import { takeWhile } from 'rxjs/operators';
import { forkJoin } from 'rxjs';
import { IwsnBackendService } from '../../../iwsn-backend/iwsn-backend.service'; 

@Component({
  selector: 'ngx-temperature',
  styleUrls: ['./temperature.component.scss'],
  templateUrl: './temperature.component.html',
})
export class TemperatureComponent implements OnDestroy {
  private alive = true;
  temperatureData: Number;
  temperature: Number;
  theme: any;

  constructor(private themeService: NbThemeService,
    public iwsnBackendService: IwsnBackendService,
    private temperatureHumidityService: TemperatureHumidityData) {
    this.themeService.getJsTheme()
      .pipe(takeWhile(() => this.alive))
      .subscribe(config => {
      this.theme = config.variables.temperature;
    });

    forkJoin(
      this.temperatureHumidityService.getTemperatureData(),
    )
    .subscribe(([temperatureData]: [Number]) => {
      this.temperatureData = temperatureData;
      this.temperature = this.temperatureData;
    });
  }

    //Refresh knop
    onRefresh(): void {
      console.log(
        this.iwsnBackendService.getMeasurements().subscribe(item => item[0].Telegram.InstantaneousElectricityUsage)
      );
      console.log("REFRESH!");
    }

  ngOnDestroy() {
    this.alive = false;
  }
}