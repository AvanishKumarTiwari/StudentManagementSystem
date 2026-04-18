const mongoose = require('mongoose');

const StudentSchema = new mongoose.Schema({
  user: { type: mongoose.Schema.Types.ObjectId, ref: 'User', required: true },
  courses: [{ type: mongoose.Schema.Types.ObjectId, ref: 'Course' }],
  attendance: [{ date: Date, present: Boolean }],
  results: [{ type: mongoose.Schema.Types.ObjectId, ref: 'Result' }],
}, { timestamps: true });

module.exports = mongoose.model('Student', StudentSchema);
