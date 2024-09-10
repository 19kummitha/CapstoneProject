using CommunityConnect.Data;
using CommunityConnect.Models;
using MediatR;

namespace CommunityConnect.Features.Admin.Commands.CreatePostCommand
{
    public class CreatePostCommand : IRequest<bool>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile Media { get; set; } 
    }
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, bool>
    {
        private readonly CommunityDbContext _dbContext;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ILogger<CreatePostCommandHandler> _logger;

        public CreatePostCommandHandler(CommunityDbContext context, IHostEnvironment hostEnvironment, ILogger<CreatePostCommandHandler> logger)
        {
            _dbContext = context;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }
        public async Task<bool> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var mediaUrl = request.Media != null ? await SaveMediaAsync(request.Media, cancellationToken) : null;

            var newPost = new Post
            {
                Title = request.Title,
                Content = request.Content,
                MediaUrl = mediaUrl
            };

            _dbContext.Posts.Add(newPost);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        private async Task<string> SaveMediaAsync(IFormFile media, CancellationToken cancellationToken)
        {
            if (media == null || media.Length == 0)
                return null;

            var fileName = Path.GetFileName(media.FileName);
            var uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "Uploads");
            var filePath = Path.Combine(uploadsFolder, fileName);

            Directory.CreateDirectory(uploadsFolder);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream, cancellationToken);
            }

            var relativeUrl = $"/uploads/{fileName}";
            return relativeUrl;
        }
    }
}
