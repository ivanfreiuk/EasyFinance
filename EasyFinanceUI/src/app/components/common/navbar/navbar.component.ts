import { Component, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { MatMenuTrigger } from '@angular/material';
import { AuthenticationService } from 'src/app/services/user/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  
  @Output() menuButtonClick = new EventEmitter();

  @ViewChild(MatMenuTrigger) trigger: MatMenuTrigger;
  
  title: string = 'Easy Finance'
  
  constructor(private authService: AuthenticationService,
    private router: Router) { }

  ngOnInit() {
  }

  onMenuButtonClick() {
    this.menuButtonClick.emit();
  }

  logout() {
    this.authService.logout();
    this.router.navigate(["/login"]);
  }
}
