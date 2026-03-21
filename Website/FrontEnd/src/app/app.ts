import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MainPage } from './Components/main-page/main-page';
import { NavBar } from './Components/nav-bar/nav-bar';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MainPage, NavBar],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('FrontEnd');
}