const express = require('express');
const router = express.Router();
const resultsController = require('../controllers/resultsController');
const { protect, authorize } = require('../middleware/auth');

router.get('/', protect, authorize('admin', 'hod', 'student'), resultsController.getAll);
router.post('/', protect, authorize('admin', 'hod'), resultsController.create);

module.exports = router;
