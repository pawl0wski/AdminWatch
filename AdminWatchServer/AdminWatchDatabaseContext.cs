using Microsoft.EntityFrameworkCore;

namespace AdminWatchServer;

public class AdminWatchContext(DbContextOptions<AdminWatchContext> context) : DbContext(context);