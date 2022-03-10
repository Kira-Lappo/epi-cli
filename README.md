# Optimizely (EPiServer) Cli

## Important!

It's not a ready-to-go solution, created only for one time import solution.

## Usage

Use `epi -h` help option to see possible commands of the utility.

Use `epi subcommand -h` to see possible options of subcommand, the same for all inner levels.

## Build

Use `publish.ps1` to publish tool.

All published binaries will be placed in `./out/$RID/` folder, where $RID - is a [Runtime IDentifier](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog)

To publish for windows platform:

```sh
./publish.ps1 -RID win-x64
```

## Configuration

epi cli reads the next sources:

- environment variables with prefix `EPICLI_`, optional
- **~/.config/epicli/config.json**, optional
- command line args, like `--api:baseUrl=http://localhost:8080`, optional

**`Important`**: **appsettings.json** is not copied at build/publish time, it is just for configuration reference

## Usage

### Import CMS Data (episerverdata)

```sh
epi import epidata --api:baseUrl=http://localhost:8080 --agr --use-chunks -f 'cms-data.episerverdata' --chunk-size=123124121
```

where:

| Argument       | Description                                                  |
| -------------- | ------------------------------------------------------------ |
| `--agr`        | import files into `Accets Global Root` (aka `For All Sites`) |
| `--use-chunks` | use upload via chunks                                        |
| `--chunk-size` | chunk max size (50 MB by default)                            |
| `-f`           | file to import                                               |

### Get Last Job status / Follow Job

```sh
epi job f <GUID>
```
