const express = require('express');
const router = express.Router();
const assignmentsController = require('../controllers/assignmentsController');
const { protect, authorize } = require('../middleware/auth');

router.get('/', protect, authorize('admin', 'hod', 'student'), assignmentsController.getAll);
router.post('/', protect, authorize('admin', 'hod', 'student'), assignmentsController.create);

module.exports = router;
