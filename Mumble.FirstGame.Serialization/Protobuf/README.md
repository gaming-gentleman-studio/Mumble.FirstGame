Command template to generate proto classes:
protoc.lnk --proto_path=%cd% --csharp_out=%cd%/Action %cd%/Action/Schema/*.proto

protoc.lnk --proto_path=%cd% --csharp_out=%cd%/ActionResult %cd%/ActionResult/Schema/*.proto