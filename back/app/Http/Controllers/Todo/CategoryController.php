<?php

namespace App\Http\Controllers\Todo;

use App\Http\Controllers\Controller;
use App\Services\CategoryService;
use Illuminate\Http\Request;

class CategoryController extends Controller
{
    private $service;
    private $storeRules = [
        'name'          => 'required|unique:posts|max:255',
        'description'   => 'required',
    ];
    function __construct()
    {
        $this->service = new CategoryService();
    }
    public function store(Request $request)
    {
        $data = $request->all();

        $validated = $request->validate($this->storeRules);
        if ($validated) {
            $name = $data["name"];
            $description = $data["description"];
            $this->service->store($name, $description);
        }
    }
    public function getCategories()
    {
        return $this->service->getCategories();
    }
}
