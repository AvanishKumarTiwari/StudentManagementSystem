const express = require('express');
const router = express.Router();
const studentsController = require('../controllers/studentsController');
const { protect, authorize } = require('../middleware/auth');

router.get('/', protect, authorize('admin', 'hod'), studentsController.getAll);
router.get('/:id', protect, authorize('admin', 'hod', 'student'), studentsController.get);
router.put('/:id', protect, authorize('admin', 'hod'), studentsController.update);

module.exports = router;
