import { Component, inject, OnInit} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { HttpClient } from '@angular/common/http';
import { product } from './shared/models/product';
import { pagination } from './shared/models/pagination';

@Component({
  selector: 'app-root',
  standalone:true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  baseUrl='https://localhost:5001/api/'
  private http=inject(HttpClient)
  //ou 2 méthode
  //constructor(private http:HttpClient){}
  title = 'SKINET';
  products:product[]=[];
  ngOnInit(): void {
    this.http.get<pagination<product>>(this.baseUrl+'products').subscribe({
      next:Response=>this.products=Response.data,
      error:error=>console.log(error),
      complete:()=>console.log('complete')
    })
  }
}
