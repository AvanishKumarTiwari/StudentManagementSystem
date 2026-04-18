const Result = require('../models/Result');

exports.getAll = async (req, res, next) => {
  try {
    const results = await Result.find().populate('student');
    res.json(results);
  } catch (err) {
    next(err);
  }
};

exports.create = async (req, res, next) => {
  try {
    const { grade, student, details } = req.body;
    const r = new Result({ grade, student, details });
    await r.save();
    res.status(201).json(r);
  } catch (err) {
    next(err);
  }
};
