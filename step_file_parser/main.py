from step_parser import StepParser

if __name__ == '__main__':
    parser = StepParser()
    step_directory = 'step_files'
    step_filename = '593D.stp'
    parser.add_file(f'{step_directory}/{step_filename}')
    parsed = parser.parse_entities()

    for key in parsed.keys():
        print(f'Key: {key}')
        for ke in parsed[key].keys():
            print(f'{ke}: {parsed[key][ke]}')

    # keys = parser.step_parse_wrapper[keys[0]].keys()
    # print(keys)
