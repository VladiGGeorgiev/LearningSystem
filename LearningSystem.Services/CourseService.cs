﻿namespace LearningSystem.Services
{
    using AutoMapper;
    using LearningSystem.Core.Dtos;
    using LearningSystem.Core.Entities;
    using LearningSystem.Core.Repositories;
    using LearningSystem.Core.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CourseService : ICourseService
    {
        private IRepository<Course> courseRepository;

        public CourseService(IRepository<Course> courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public CourseDetailDto GetCourse(int id)
        {
            var course = this.courseRepository
                .Include(x => x.Lectures)
                .ThenInclude<Lecture, List<HomeworkAssignment>>(x => x.HomeworkAssignments)
                .ThenInclude<HomeworkAssignment, List<HomeworkSubmission>>(x => x.HomeworkSubmissions)                
                .Get(id);
            var courseDto = Mapper.Map<CourseDetailDto>(course);
            
            return courseDto;
        }
    }
}
