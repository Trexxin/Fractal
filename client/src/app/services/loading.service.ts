import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { LOADERS } from 'ngx-spinner/lib/ngx-spinner.enum';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  loadingRequestCount = 0;

  constructor(private spinnerService: NgxSpinnerService) { }

  loading() {
    this.loadingRequestCount++;
    this.spinnerService.show(undefined, {
      type: "square-loader",
      bdColor: 'rgba(255,255,255,0)',
      color: "#282828"
    });
  }

  idle() {
    this.loadingRequestCount--;
    if (this.loadingRequestCount <= 0) {
      this.loadingRequestCount = 0;
      this.spinnerService.hide();
    }
  }
}
