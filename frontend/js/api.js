// Example frontend helper functions using fetch and localStorage for JWT
const API_URL = 'http://localhost:5000/api';

async function request(path, options = {}) {
  const token = localStorage.getItem('token');
  const headers = options.headers || {};
  if (!headers['Content-Type'] && !(options.body instanceof FormData)) headers['Content-Type'] = 'application/json';
  if (token) headers['Authorization'] = `Bearer ${token}`;

  const res = await fetch(`${API_URL}${path}`, { ...options, headers });
  const text = await res.text();
  let data = null;
  try { data = text ? JSON.parse(text) : null; } catch (e) { data = text; }

  if (!res.ok) {
    throw data || { message: 'Request failed' };
  }
  return data;
}

export async function login(data) {
  return request('/login', { method: 'POST', body: JSON.stringify(data) });
}

export async function getDashboard() {
  return request('/student/dashboard');
}

export async function getCourses() {
  return request('/student/courses');
}

export async function getAssignments() {
  return request('/student/assignments');
}

export async function submitAssignment(payload) {
  return request('/student/assignments/submit', { method: 'POST', body: JSON.stringify(payload) });
}

export async function getProfile() {
  return request('/student/profile');
}

export async function updateProfile(data) {
  return request('/student/profile', { method: 'PUT', body: JSON.stringify(data) });
}

export function logout() {
  localStorage.removeItem('token');
  localStorage.removeItem('user');
  window.location = '/auth/login.html';
}
