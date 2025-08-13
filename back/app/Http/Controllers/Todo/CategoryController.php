<?php

namespace App\Http\Controllers\Todo;

use App\Http\Controllers\Controller;
use App\Services\CategoryService;
use Illuminate\Http\Request;

class CategoryController extends Controller
{
    private $service;
    function __construct(){
        $this->service = new CategoryService();
    }
    public function store(Request $request) {
        $data = $request->all();
        return $data;
        $this->service->store();
    }
    public function getCategories() {
        return $this->service->getCategories();
    }
}
