import { getDashboard, getCourses, getAssignments } from './api.js';

export async function loadDashboard() {
  try {
    const stats = await getDashboard();
    document.querySelector('#totalCourses').textContent = stats.totalCourses ?? 0;
    document.querySelector('#totalAssignments').textContent = stats.totalAssignments ?? 0;
    document.querySelector('#attendance').textContent = stats.attendance ?? '0%';
    document.querySelector('#result').textContent = stats.result ?? 'N/A';

    // populate courses table
    const courses = await getCourses();
    const tbody = document.querySelector('#myCoursesTbody');
    if (tbody) {
      tbody.innerHTML = '';
      courses.forEach(c => {
        const tr = document.createElement('tr');
        tr.innerHTML = `<td>${c._id}</td><td>${c.title}</td><td>${c.faculty || ''}</td><td>${c.status}</td>`;
        tbody.appendChild(tr);
      });
    }

    // populate assignments
    const assignments = await getAssignments();
    const list = document.querySelector('#assignmentsList');
    if (list) {
      list.innerHTML = '';
      assignments.forEach(a => {
        const li = document.createElement('li');
        li.className = 'list-group-item d-flex justify-content-between align-items-center';
        li.innerHTML = `${a.title} <span class="badge bg-secondary">${a.status}</span>`;
        list.appendChild(li);
      });
    }
  } catch (err) {
    console.error(err);
    // If unauthorized or token expired, redirect to login
    if (err && err.message === 'Not authorized') {
      window.location = '/auth/login.html';
    }
  }
}
