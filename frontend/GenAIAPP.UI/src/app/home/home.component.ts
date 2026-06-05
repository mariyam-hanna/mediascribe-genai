import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-home',
    standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  message = '';

  constructor(
    private router: Router,
    private apiService: ApiService
  ) { }

  ngOnInit() {
    this.apiService.testConnection().subscribe({
      next: (response: any) => {
        this.message = response.message;
      },
      error: (error:any) => {
        console.error(error);
        this.message = 'Connection failed';
      }
    });
  }

  navigateToUpload() {
    this.router.navigate(['/upload']);
  }
}