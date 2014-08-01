# Deletes the temp files and Compiler-Generated Directory
import os
import shutil

""" Gets a list of files using a extension filter"""
def getFiles(relevant_path):
    included_extenstions = ['pdb','txt','config','xml' ]
    file_names = [fn for fn in os.listdir(relevant_path) if any([fn.endswith(ext) for ext in included_extenstions])]
    return file_names
# get the files to delete from build directory
files_to_delete = getFiles("Build")
# delete the files
for file in files_to_delete:
    os.remove("Build\\"+file)
""" Deletes a directory along with all it's contents"""
def deleteDir(path):
    if(os.path.isdir(path)):
        shutil.rmtree(path)
    else:
        print("Directory Deosn't exist")

# delete all directories
deleteDir("SS.Ynote.Classic\\obj")
deleteDir("Docking\\bin")
deleteDir("Docking\\obj")
deleteDir("FastColoredTextBox\\bin")
deleteDir("FastColoredTextBox\\obj")
