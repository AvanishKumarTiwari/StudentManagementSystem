const Student = require('../models/Student');
const User = require('../models/User');

exports.getAll = async (req, res, next) => {
  try {
    const students = await Student.find().populate('user', 'name email').populate('courses');
    res.json(students);
  } catch (err) {
    next(err);
  }
};

exports.get = async (req, res, next) => {
  try {
    const student = await Student.findById(req.params.id).populate('user', 'name email').populate('courses').populate('results');
    if (!student) return res.status(404).json({ message: 'Student not found' });
    res.json(student);
  } catch (err) {
    next(err);
  }
};

exports.update = async (req, res, next) => {
  try {
    const student = await Student.findById(req.params.id);
    if (!student) return res.status(404).json({ message: 'Student not found' });

    // update allowed fields
    const { courses, attendance } = req.body;
    if (courses) student.courses = courses;
    if (attendance) student.attendance = attendance;

    await student.save();
    res.json(student);
  } catch (err) {
    next(err);
  }
};
