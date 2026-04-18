const mongoose = require('mongoose');

const ResultSchema = new mongoose.Schema({
  grade: { type: String, required: true },
  student: { type: mongoose.Schema.Types.ObjectId, ref: 'Student', required: true },
  details: { type: String },
}, { timestamps: true });

module.exports = mongoose.model('Result', ResultSchema);
