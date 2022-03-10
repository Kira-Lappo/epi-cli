namespace OptimizelyApiClient.Cli;

public static class FileStreamExtensions
{
	public static IEnumerable<Chunk> GetChunks(this FileStream fileStream, int maxChunkSize)
	{
		using var bs = new BufferedStream(fileStream);
		while (true) //reading 1mb chunks at a time
		{
			var buffer = new byte[maxChunkSize];
			var bytesRead = bs.Read(buffer, 0, maxChunkSize);
			if (bytesRead <= 0)
			{
				yield break;
			}

			yield return new Chunk
			{
				Bytes = buffer,
				Length = bytesRead,
			};
		}
	}

	public readonly struct Chunk
	{
		public byte[] Bytes { get; init; }

		public int Length { get; init; }

		public void Deconstruct(out byte[] bytes, out int length)
		{
			bytes  = Bytes;
			length = Length;
		}
	}
}
