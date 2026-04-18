const Course = require('../models/Course');

exports.getAll = async (req, res, next) => {
  try {
    const courses = await Course.find();
    res.json(courses);
  } catch (err) {
    next(err);
  }
};

exports.create = async (req, res, next) => {
  try {
    const { name, faculty, status } = req.body;
    const course = new Course({ name, faculty, status });
    await course.save();
    res.status(201).json(course);
  } catch (err) {
    next(err);
  }
};
