import { login } from './api.js';

export async function handleLogin(e) {
  e.preventDefault();
  const email = document.querySelector('#email').value;
  const password = document.querySelector('#password').value;

  try {
    const res = await login({ email, password });
    localStorage.setItem('token', res.token);
    localStorage.setItem('user', JSON.stringify(res.user));
    // redirect based on role
    if (res.user.role === 'admin') window.location = '/admin/dashboard.html';
    else if (res.user.role === 'hod') window.location = '/hod/dashboard.html';
    else window.location = '/student/dashboard.html';
  } catch (err) {
    alert(err.message || 'Login failed');
  }
}

export function protectRoute() {
  const token = localStorage.getItem('token');
  if (!token) window.location = '/auth/login.html';
}
