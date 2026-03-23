import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

export interface SignInResponse {
  token: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {

  private baseUrl = '/api'; // proxy to your ASP.NET backend

  constructor(private http: HttpClient) {}

  /**
   * Standard email/password sign-in.
   * POST /api/auth/login  →  { token: string }
   */
  async signIn(email: string, password: string): Promise<string> {
    const res = await firstValueFrom(
      this.http.post<SignInResponse>(`${this.baseUrl}/auth/login`, { email, password })
    );
    return res.token;
  }

  /**
   * Retrieve the stored JWT (checks both storage types).
   */
  getToken(): string | null {
    return localStorage.getItem('auth_token') ?? sessionStorage.getItem('auth_token');
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  signOut(): void {
    localStorage.removeItem('auth_token');
    sessionStorage.removeItem('auth_token');
  }
}