import React, { useState } from 'react';

const CourseCreateForm = () => {
    const [course, setCourse] = useState({
        courseName: '',
        description: '',
        content: '',
        startDate: '',
        endDate: '',
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setCourse({
            ...course,
            [name]: value,
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(course);
    };

    return (
        <form onSubmit={handleSubmit}>
            <div className="form-group">
                <label htmlFor="courseName" className="control-label">Course Name</label>
                <input
                    type="text"
                    name="courseName"
                    className="form-control"
                    value={course.courseName}
                    onChange={handleChange}
                />
                <span className="text-danger"></span>
            </div>
            <div className="form-group">
                <label htmlFor="description" className="control-label">Description</label>
                <textarea
                    name="description"
                    className="form-control"
                    value={course.description}
                    onChange={handleChange}
                ></textarea>
                <span className="text-danger"></span>
            </div>
            <div className="form-group">
                <label htmlFor="content" className="control-label">Content</label>
                <textarea
                    name="content"
                    className="form-control"
                    value={course.content}
                    onChange={handleChange}
                ></textarea>
                <span className="text-danger"></span>
            </div>
            <div className="form-group">
                <label htmlFor="startDate" className="control-label">Start Date</label>
                <input
                    type="date"
                    name="startDate"
                    className="form-control"
                    value={course.startDate}
                    onChange={handleChange}
                />
                <span className="text-danger"></span>
            </div>
            <div className="form-group">
                <label htmlFor="endDate" className="control-label">End Date</label>
                <input
                    type="date"
                    name="endDate"
                    className="form-control"
                    value={course.endDate}
                    onChange={handleChange}
                />
                <span className="text-danger"></span>
            </div>
            <div className="form-group">
                <input type="submit" value="Create" className="btn btn-primary" />
            </div>
        </form>
    );
};

export default CourseCreateForm;
