#!/usr/bin/env sh

# Synopsis:
# Update the expected files for the golden tests

bin/run-tests-in-docker.sh

find tests -name representation.txt  -execdir cp {} expected_representation.txt \;
find tests -name representation.json -execdir cp {} expected_representation.json \;
find tests -name mapping.json        -execdir cp {} expected_mapping.json \;
