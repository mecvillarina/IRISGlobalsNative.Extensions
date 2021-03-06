# IRISGlobalsNative.Extensions
This library currently have two core functionality: Class-based Collection for Globals and Json Provider for Globals.

## Inspiration
Since C# is a strongly typed language. We developers mostly uses classes to store some set of information and hence, we want everything parsable mostly from string to our class models or vice versa.

## What it does
This library currently have two core functionality: Class-based Collection for Globals and Json Provider for Globals.

- One of the main feature of IRISGlobalsNative.Extensions is the Class-based Collection for Global. This is pretty helpful when you need to store temporary collection/s based from your model classes into your Global. You can utilize LINQ's capabilities without worrying about your Globals. All the collections will be saved at the single global named "^CollectionGlobals".

- Another feature of IRISGlobalsNative.Extensions is Json Provider for Globals. This is pretty helpful when you want to import your json data into the globals or export your imported json data from the globals. The library will extract all the possible paths/nodes and its value from your json file and save it individually into the specified Global.

## Challenges I ran into
During the development, I always test all the functionality thoroughly, because I don't want this library to be somewhat show-stopper on what Native API can offer interms of performance.

Also, I encountered a lot of challenges in docker. Since, it is my first time to use it.

## Accomplishments that I proud of
Well, it was my first time to use IRIS Intersystem platform and I don't have any major challenges or blocker in terms of development.

## What I learned
I learned a lot in terms of what Native API can offer.

## Built with
Using VSCode / Visual Studio 2019, IRIS Community Edition in Docker, IRIS Native API for .NET

## Installation with Docker

## Prerequisites
Make sure you have [git](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git) and [Docker desktop](https://www.docker.com/products/docker-desktop) installed.


Clone/git pull the repo into any local directory e.g. like it is shown below:

```
$ git clone https://github.com/mecvillarina/IRISGlobalsNative.Extensions.git
```

Open the terminal in this directory and run:

```
$ docker-compose up -d --build
```

## How to Work With it

Open localhost:9092 in browser.

That's the sample web app that uses the library I made that uses IRIS Native API for .NET.