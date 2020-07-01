To properly serialize/deserialize an action/actionResult
1. In Action/Schema or ActionResult/Schema, create a new proto file defining the serialized version of the action/actionResult
2. In the Protobuf folder, run the appropriate Command template to generate proto classes:
   a. protoc.lnk --proto_path=%cd% --csharp_out=%cd%/Action %cd%/Action/Schema/*.proto
   b. protoc.lnk --proto_path=%cd% --csharp_out=%cd%/ActionResult %cd%/ActionResult/Schema/*.proto
3. In Factory/ActionFactory or Factory/ActionResultFactory, add a line to create an action/actionResult from the proto object, and a line to create a proto object from an action/actionResult
4. In Lookup.cs, add a new const int representing the type of action/actionResult you made, and add it to the dictionary as well

If you create a new entity, you also need to update EntityFactory and Lookup.cs as well

