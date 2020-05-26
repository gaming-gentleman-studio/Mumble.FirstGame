Command template to generate proto classes:
protoc.lnk --proto_path=%cd%/Action/Schema/ --csharp_out=%cd%/Action %cd%/Action/Schema/*.proto

protoc.lnk --proto_path=%cd%/ActionResult/Schema/ --csharp_out=%cd%/ActionResult %cd%/ActionResult/Schema/*.proto