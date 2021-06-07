import {Component, Inject} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  resultsOfEvaluation: string[];
  baseUrl: string;
  http: HttpClient;
  showProgress = false;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  private onSubmit(value): void {
    //Show "Loading..."
    this.showProgress = true;

    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    //Send user's text to API
    this.http.post<string[]>(this.baseUrl + 'home', JSON.stringify(value), {headers : headers})
      .subscribe(result => {
      this.resultsOfEvaluation = result;
      //Stop showing "Loading..." after data is returned from API
      this.showProgress = false;
    }, error => console.error(error));
  }
}
