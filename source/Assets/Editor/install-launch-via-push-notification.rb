system "cp -r #{library_path} #{project_directory}/"
project.save_as("#{project_directory}/#{project_file}")

puts "Copy UIApplication+GrowthPush category to project..."
library_directory = File.dirname(library_path)
system "cp #{library_directory}/UIApplication+GrowthPush.* #{project_directory}/"

puts "Add UIApplication+GrowthPush category to build target..."
file_reference = project.main_group.new_file("#{project_directory}/UIApplication+GrowthPush.h")
target.headers_build_phase.add_file_reference(file_reference)
file_reference = project.main_group.new_file("#{project_directory}/UIApplication+GrowthPush.m")
target.source_build_phase.add_file_reference(file_reference)

puts "Update project files..."
project.save_as("#{project_directory}/#{project_file}")
