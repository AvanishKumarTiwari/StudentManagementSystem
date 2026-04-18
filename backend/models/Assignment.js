const mongoose = require('mongoose');

const AssignmentSchema = new mongoose.Schema({
  title: { type: String, required: true },
  description: { type: String },
  status: { type: String, enum: ['pending', 'submitted', 'graded'], default: 'pending' },
  student: { type: mongoose.Schema.Types.ObjectId, ref: 'Student' },
}, { timestamps: true });

module.exports = mongoose.model('Assignment', AssignmentSchema);
