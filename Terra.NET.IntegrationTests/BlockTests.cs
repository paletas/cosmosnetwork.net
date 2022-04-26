using FluentAssertions;
using FluentAssertions.Execution;
using System.Threading.Tasks;
using Xunit;

namespace Terra.NET.IntegrationTests
{
    public class BlockTests : IClassFixture<TestTerraApi>
    {
        private readonly TerraApi _api;

        public BlockTests(TestTerraApi api)
        {
            this._api = api;
        }

        [Fact]
        public async Task GetLatestBlock()
        {
            using var _ = new AssertionScope();

            var block = await this._api.Blocks.GetLatestBlock();

            block.Should().NotBeNull();
            block.BlockId.Should().NotBeNull();
            block.Details.Should().NotBeNull();
        }

        [Fact]
        public async Task GetBlockAtHeight()
        {
            const ulong BlockHeight = 6000001;

            using var _ = new AssertionScope();

            var block = await this._api.Blocks.GetBlock(BlockHeight);

            block.Should().NotBeNull();

            if (block is not null)
            {
                block.BlockId.Should().NotBeNull();
                block.Details.Should().NotBeNull();
                block.Details.Header.Height.Should().Be(BlockHeight);
            }
        }

        [Fact]
        public async Task GetLatestBlock_ShouldBeEquivalent_ToBlockAtHeight()
        {
            using var _ = new AssertionScope();

            var latestBlock = await this._api.Blocks.GetLatestBlock();
            var block = await this._api.Blocks.GetBlock(latestBlock.Details.Header.Height);

            latestBlock.Should().NotBeNull();
            block.Should().NotBeNull();

            if (block is not null)
            {
                block.Should().BeEquivalentTo(latestBlock);
            }
        }
    }
}