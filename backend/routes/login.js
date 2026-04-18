const express = require('express');
const router = express.Router();
const { body } = require('express-validator');
const authController = require('../controllers/authController');

// For compatibility, reuse authController.login but expose at /api/login
router.post('/', [
  body('email').isEmail().withMessage('Valid email is required'),
  body('password').exists().withMessage('Password is required')
], authController.login);

module.exports = router;
