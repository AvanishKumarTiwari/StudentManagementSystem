const express = require('express');
const router = express.Router();
const coursesController = require('../controllers/coursesController');
const { protect, authorize } = require('../middleware/auth');

router.get('/', protect, authorize('admin', 'hod', 'student'), coursesController.getAll);
router.post('/', protect, authorize('admin', 'hod'), coursesController.create);

module.exports = router;
