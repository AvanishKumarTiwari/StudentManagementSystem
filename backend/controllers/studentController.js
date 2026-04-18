const Student = require('../models/Student');
const Course = require('../models/Course');
const Assignment = require('../models/Assignment');
const User = require('../models/User');

exports.dashboard = async (req, res, next) => {
  try {
    const userId = req.user._id;
    const student = await Student.findOne({ user: userId }).populate('courseIds');
    if (!student) return res.status(404).json({ message: 'Student profile not found' });

    const totalCourses = student.courseIds.length;
    const totalAssignments = await Assignment.countDocuments({ student: student._id });
    const attendance = student.attendance || 0;
    const result = student.result || 'N/A';

    res.json({ totalCourses, totalAssignments, attendance, result });
  } catch (err) {
    next(err);
  }
};

exports.getCourses = async (req, res, next) => {
  try {
    const userId = req.user._id;
    const student = await Student.findOne({ user: userId }).populate('courseIds');
    if (!student) return res.status(404).json({ message: 'Student profile not found' });
    res.json(student.courseIds);
  } catch (err) {
    next(err);
  }
};

exports.getAssignments = async (req, res, next) => {
  try {
    const userId = req.user._id;
    const student = await Student.findOne({ user: userId });
    if (!student) return res.status(404).json({ message: 'Student profile not found' });

    const assignments = await Assignment.find({ student: student._id });
    res.json(assignments);
  } catch (err) {
    next(err);
  }
};

exports.submitAssignment = async (req, res, next) => {
  try {
    const { assignmentId } = req.body;
    const assignment = await Assignment.findById(assignmentId);
    if (!assignment) return res.status(404).json({ message: 'Assignment not found' });

    // ensure assignment belongs to the student
    const student = await Student.findOne({ user: req.user._id });
    if (!student) return res.status(404).json({ message: 'Student profile not found' });
    if (!assignment.student.equals(student._id)) return res.status(403).json({ message: 'Not allowed' });

    assignment.status = 'submitted';
    await assignment.save();
    res.json({ message: 'Submitted', assignment });
  } catch (err) {
    next(err);
  }
};

exports.getProfile = async (req, res, next) => {
  try {
    const user = await User.findById(req.user._id).select('-password');
    if (!user) return res.status(404).json({ message: 'User not found' });
    const student = await Student.findOne({ user: user._id }).populate('courseIds');
    res.json({ user, student });
  } catch (err) {
    next(err);
  }
};

exports.updateProfile = async (req, res, next) => {
  try {
    const { name, email, password } = req.body;
    const user = await User.findById(req.user._id);
    if (!user) return res.status(404).json({ message: 'User not found' });

    if (name) user.name = name;
    if (email) user.email = email;
    if (password) {
      const bcrypt = require('bcrypt');
      const salt = await bcrypt.genSalt(10);
      user.password = await bcrypt.hash(password, salt);
    }
    await user.save();
    res.json({ message: 'Profile updated', user: { id: user._id, name: user.name, email: user.email } });
  } catch (err) {
    next(err);
  }
};
