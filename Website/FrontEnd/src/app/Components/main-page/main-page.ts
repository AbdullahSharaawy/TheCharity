import { Component } from '@angular/core';
import { HeroSlider } from '../hero-slider/hero-slider';
import { Welcome } from '../welcome/welcome';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [Welcome, HeroSlider],
  templateUrl: './main-page.html',
  styleUrl: './main-page.css',
})
export class MainPage {}