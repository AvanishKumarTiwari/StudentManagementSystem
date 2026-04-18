const express = require('express');
const router = express.Router();
const { protect, authorize } = require('../middleware/auth');
const studentController = require('../controllers/studentController');

// All routes protected and only for students
router.use(protect);
router.use(authorize('student'));

router.get('/dashboard', studentController.dashboard);
router.get('/courses', studentController.getCourses);
router.get('/assignments', studentController.getAssignments);
router.post('/assignments/submit', studentController.submitAssignment);
router.get('/profile', studentController.getProfile);
router.put('/profile', studentController.updateProfile);

module.exports = router;
