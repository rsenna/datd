@echo off

netsh http delete urlacl url=http://+:53991/
netsh http add urlacl url=http://+:53991/ user=Everyone

pause