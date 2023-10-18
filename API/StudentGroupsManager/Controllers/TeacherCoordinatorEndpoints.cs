using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using StudentGroupsManager.Data;
using StudentGroupsManager.Entity;
namespace StudentGroupsManager.Controllers;

public static class TeacherCoordinatorEndpoints
{
    public static void MapTeacherCoordinatorEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/TeacherCoordinator").WithTags(nameof(TeacherCoordinator));

        group.MapGet("/", async (StudentGroupsManagerContext db) =>
        {
            return await db.TeacherCoordinators.ToListAsync();
        })
        .WithName("GetAllTeacherCoordinators")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<TeacherCoordinator>, NotFound>> (int id, StudentGroupsManagerContext db) =>
        {
            return await db.TeacherCoordinators.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is TeacherCoordinator model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetTeacherCoordinatorById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, TeacherCoordinator teacherCoordinator, StudentGroupsManagerContext db) =>
        {
            var affected = await db.TeacherCoordinators
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Name, teacherCoordinator.Name)
                  .SetProperty(m => m.Mail, teacherCoordinator.Mail)
                  .SetProperty(m => m.RP, teacherCoordinator.RP)
                  .SetProperty(m => m.Password, teacherCoordinator.Password)
                  //.SetProperty(m => m.Id, teacherCoordinator.Id)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateTeacherCoordinator")
        .WithOpenApi();

        group.MapPost("/", async (TeacherCoordinator teacherCoordinator, StudentGroupsManagerContext db) =>
        {
            db.TeacherCoordinators.Add(teacherCoordinator);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/TeacherCoordinator/{teacherCoordinator.Id}",teacherCoordinator);
        })
        .WithName("CreateTeacherCoordinator")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, StudentGroupsManagerContext db) =>
        {
            var affected = await db.TeacherCoordinators
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteTeacherCoordinator")
        .WithOpenApi();
    }
}
