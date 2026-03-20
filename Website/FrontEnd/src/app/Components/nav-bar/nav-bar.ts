import {
  Component,
  HostListener,
  signal,
  computed,
  OnInit,
  OnDestroy,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterLinkActive, RouterLink } from '@angular/router';

export interface NavLink {
  label: string;
  href: string;
  children?: NavLink[];
}

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './nav-bar.html',
  styleUrls: ['./nav-bar.css'],
})
export class NavBar implements OnInit, OnDestroy {
  /* - Navigation data - */
  links: NavLink[] = [
    { label: 'Journal', href: '/journal' },
    { label: 'About', href: '/about' },
  ];

  activeDropdown = signal<string | null>(null);
  
  ngOnInit(): void {}
  ngOnDestroy(): void {
    document.body.style.overflow = '';
  }

  /* Desktop dropdown */
  openDropdown(label: string): void  { this.activeDropdown.set(label); }
  closeDropdown(): void              { this.activeDropdown.set(null); }
}