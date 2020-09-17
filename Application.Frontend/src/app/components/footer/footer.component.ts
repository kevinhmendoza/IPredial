import { Component, OnInit } from '@angular/core';
import { SummaryResumeService } from 'src/app/services/summary-resume/summary-resume.service';
import { Summary } from 'src/app/entities/summary';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {
  test: Date = new Date();

  constructor(private _summaryResumeService: SummaryResumeService) { }
  public Summary: Summary;
  ngOnInit() {
    this.getData();
  }

  private getData(): void {
    this._summaryResumeService.GetProfile().subscribe(data => {
      this.Summary = data;
    });
  }

}
