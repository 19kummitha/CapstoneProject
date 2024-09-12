using CommunityConnect.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnect.Features.Admin.Commands.DeletePostCommand
{
    public class DeletePostCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
    {
        private CommunityDbContext _context;
        public DeletePostCommandHandler(CommunityDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var postTodelete = await _context.Posts.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (postTodelete == null)
            {
                return false;
            }

            _context.Posts.Remove(postTodelete);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
