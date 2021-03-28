import { Injectable } from '@angular/core';
import { of as observableOf, Observable } from 'rxjs';
import { Electricity, ElectricityChart, ElectricityData } from '../data/electricity';
import { IwsnBackendService } from '../../iwsn-backend/iwsn-backend.service';

@Injectable()
export class ElectricityService extends ElectricityData {
  private listData: Electricity[] = [
    {
      title: '2021',
      months: [

      ],
    },
  ];

  private chartPoints = [
    490, 490, 495, 500,
    505, 510, 520, 530,
    550, 580, 630, 720,
    800, 840, 860, 870,
    870, 860, 840, 800,
    720, 200, 145, 130,
    130, 145, 200, 570,
    635, 660, 670, 670,
    660, 630, 580, 460,
    380, 350, 340, 340,
    340, 340, 340, 340,
    340, 340, 340,
  ];

  chartData: ElectricityChart[];

  constructor(public iwsnBackendService: IwsnBackendService) {
    super();
    this.iwsnBackendService.getMeasurements().subscribe(item => console.log(item));
    this.chartData = this.chartPoints.map((p, index) => ({
      //Creating of labels
      label: (index % 10 === 0) ? `${Math.round(index / 10)}` : '',
      value: p,
    }));
  }

  //Not in use, but needed due to inheritance
  getListData(): Observable<Electricity[]> {
    return observableOf(this.listData);
  }

  getChartData(): Observable<ElectricityChart[]> {
    return observableOf(this.chartData);
  }
}