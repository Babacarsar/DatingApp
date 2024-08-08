import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating APP';
  users: any;
  constructor(private http: HttpClient){}
  ngOnInit(){
    this.getUsers();
  }
  getUsers (){
    this.http.get('http://localhost:5031/api/users').subscribe(Response=>{
      this.users = Response;
    },error =>{
      console.log(error);
    })

  }
}
