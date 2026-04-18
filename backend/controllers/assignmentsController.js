const Assignment = require('../models/Assignment');

exports.getAll = async (req, res, next) => {
  try {
    const items = await Assignment.find().populate('student');
    res.json(items);
  } catch (err) {
    next(err);
  }
};

exports.create = async (req, res, next) => {
  try {
    const { title, description, student } = req.body;
    const item = new Assignment({ title, description, student });
    await item.save();
    res.status(201).json(item);
  } catch (err) {
    next(err);
  }
};
